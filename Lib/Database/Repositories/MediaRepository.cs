using AutoMapper;
using Database.DTOs;
using Database.Internal;
using Database.Internal.Models;
using Database.Repositories.Interfaces;
using System.Linq;

namespace Database.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public MediaRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public MediaSummary Create(MediaSaveNewData saveData)
        {
            var newMedia = _mapper.Map<Media>(saveData);
            _databaseContext.Medias.Add(newMedia);
            _databaseContext.SaveChanges();

            return _mapper.Map<MediaSummary>(newMedia);
        }

        public void Delete(long id)
        {
            var media = _databaseContext.Medias.Find(id);
            _databaseContext.Medias.Remove(media);
            _databaseContext.SaveChanges();
        }

        public MediaDetails Fetch(long id)
        {
            var media = _databaseContext.Medias.Find(id);
            if (media == null)
            {
                return null;
            }
            return _mapper.Map<MediaDetails>(media);
        }

        public SearchResults<MediaSummary> Search(SearchParameters parameters)
        {
            var foundMedias = _databaseContext.Medias.AsQueryable();

            var mediasPage = foundMedias
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return new SearchResults<MediaSummary>
            {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = mediasPage.Count(),
                TotalCount = foundMedias.Count(),
                Data = mediasPage.Select(article => _mapper.Map<MediaSummary>(article))
            };
        }

        public void Update(long id, MediaSaveExistingData saveData)
        {
            var media = _databaseContext.Medias.Find(id);
            _mapper.Map(saveData, media);
            _databaseContext.SaveChanges();
        }
    }
}
