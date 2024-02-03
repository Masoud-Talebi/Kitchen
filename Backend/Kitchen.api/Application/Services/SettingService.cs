using AutoMapper;
using Kitchen.api.Application.Constract;
using Kitchen.api.Application.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.api.Application.Services
{
    public class SettingService : ISettingService
    {
        private readonly SqlserverApplicationContext _context;
        private readonly IMapper _mapper;
        public SettingService(SqlserverApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<SettingDTO> GetSetting()
        {
            var setting = await _context.Settings.FirstOrDefaultAsync();
            var settingdto = _mapper.Map<SettingDTO>(setting);
            return settingdto;
        }

        public async Task<SettingDTO> UpdateSetting(SettingDTO settingdto)
        {
            var setting = _mapper.Map<Setting>(settingdto);
            _context.Settings.Update(setting);
            await _context.SaveChangesAsync();
            return _mapper.Map<SettingDTO>(setting);
        }
    }
}
