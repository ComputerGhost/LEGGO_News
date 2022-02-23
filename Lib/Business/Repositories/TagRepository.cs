using AutoMapper;
using Business.DTOs;
using Business.Repositories.Interfaces;
using Data;
using Data.Models;
using System.Linq;

namespace Business.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public TagRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public TagSummary Create(TagSaveData saveData)
        {
            var newTag = _mapper.Map<Tag>(saveData);
            _databaseContext.Tag.Add(newTag);
            _databaseContext.SaveChanges();

            return _mapper.Map<TagSummary>(newTag);
        }

        public void Update(long id, TagSaveData saveData)
        {
            var tag = _databaseContext.Tag.Find(id);
            _mapper.Map(saveData, tag);
            _databaseContext.SaveChanges();
        }

        public TagDetails Fetch(long id)
        {
            var tag = _databaseContext.Tag.Find(id);
            if (tag == null)
            {
                return null;
            }
            return _mapper.Map<TagDetails>(tag);
        }

        public SearchResults<TagSummary> Search(SearchParameters parameters)
        {
            var foundTags = _databaseContext.Tag.AsQueryable();
            if (!string.IsNullOrEmpty(parameters.Query))
            {
                foundTags = foundTags.Where(tag => tag.Name.Contains(parameters.Query));
            }

            var tagsPage = foundTags
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return new SearchResults<TagSummary>
            {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = tagsPage.Count(),
                TotalCount = foundTags.Count(),
                Data = tagsPage.Select(tag => _mapper.Map<TagSummary>(tag))
            };
        }

        public void Delete(long id)
        {
            var tag = _databaseContext.Tag.Find(id);
            _databaseContext.Tag.Remove(tag);
            _databaseContext.SaveChanges();
        }
    }
}
