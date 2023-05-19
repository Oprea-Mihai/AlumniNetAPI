using AlumniNetAPI.Repository.Interfaces;
using AlumniNetAPI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AlumniNetAPI.Controllers
{
    public class NotificationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
          
                _unitOfWork = unitOfWork;
                _mapper = mapper;
             
        }
    }
}
