using AutoMapper;
using Database.DTOs;
using Database.Internal;
using Database.Internal.Models;
using Database.Repositories.Interfaces;
using System.Linq;

namespace Database.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public LeadRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public LeadSummary Create(LeadSaveData saveData)
        {
            var newLead = _mapper.Map<Lead>(saveData);
            _databaseContext.Leads.Add(newLead);
            _databaseContext.SaveChanges();

            return _mapper.Map<LeadSummary>(newLead);
        }

        public void Delete(long id)
        {
            var lead = _databaseContext.Leads.Find(id);
            _databaseContext.Leads.Remove(lead);
            _databaseContext.SaveChanges();
        }

        public LeadDetails Fetch(long id)
        {
            var lead = _databaseContext.Leads.Find(id);
            if (lead == null)
            {
                return null;
            }
            return _mapper.Map<LeadDetails>(lead);
        }

        public SearchResults<LeadSummary> Search(SearchParameters parameters)
        {
            var foundLeads = _databaseContext.Leads.AsQueryable();

            var leadsPage = foundLeads
                .Skip(parameters.Offset)
                .Take(parameters.Count);

            return new SearchResults<LeadSummary>
            {
                Key = parameters.Key,
                Offset = parameters.Offset,
                Count = leadsPage.Count(),
                TotalCount = foundLeads.Count(),
                Data = leadsPage.Select(article => _mapper.Map<LeadSummary>(article))
            };
        }

        public void Update(long id, LeadSaveData saveData)
        {
            var lead = _databaseContext.Leads.Find(id);
            _mapper.Map(saveData, lead);
            _databaseContext.SaveChanges();
        }
    }
}
