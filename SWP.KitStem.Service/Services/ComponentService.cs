using AutoMapper;
using Microsoft.AspNetCore.Http;
using SWP.KitStem.Repository;
using SWP.KitStem.Repository.Models;
using SWP.KitStem.Service.BusinessModels.RequestModel;

namespace SWP.KitStem.Service.Services
{
    public class ComponentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ComponentService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ResponseService> DeleteComponent(int id)
        {
            try
            {
                var component = await _unitOfWork.Components.GetByIdAsync(id);
                if (component == null)
                {
                    return new ResponseService()
                        .SetSucceeded(false)
                        .SetStatusCode(StatusCodes.Status404NotFound)
                        .AddDetail("message", "Delete failed!")
                        .AddError("notFound", "Cannot found component");
                }
                _unitOfWork.Components.Delete(component);
                await _unitOfWork.SaveAsync();
                return new ResponseService()
                            .SetSucceeded(true)
                            .AddDetail("message", "Delete successed!");
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Delete failed!")
                    .AddError("outOfService", "Cannot delete!");
            }
        }
        public async Task<ResponseService> UpdateComponentAsync(ComponentUpdateRequest request)
        {
            try
            {

                var component = await _unitOfWork.Components.GetByIdAsync(request.Id);
                if (component == null)
                {
                    return new ResponseService()
                       .SetSucceeded(false)
                       .SetStatusCode(StatusCodes.Status404NotFound)
                       .AddDetail("message", "Update failed!")
                       .AddError("notFound", "Cannot found component");
                }

                component.Id = request.Id;
                component.TypeId = request.TypeId;
                component.Name = request.Name;
                component.Status = true;

                await _unitOfWork.Components.Update(component);
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "Update successed");
            }
            catch
            {
                return new ResponseService()
                        .SetSucceeded(false)
                        .AddDetail("message", "Update failed!")
                        .AddError("outOfSercive", "Cannot update");
            }
        }
        public async Task<ResponseService> CreateComponentAsync(ComponentCreateRequest request)
        {
            try
            {
                var newComponent = new Component()
                {
                    TypeId = request.TypeId,
                    Name = request.Name,
                    Status = true,
                };
                await _unitOfWork.Components.InsertAsync(newComponent);
                return new ResponseService()
                            .SetSucceeded(true)
                            .AddDetail("message", "Create component successed");
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Create component failed")
                    .AddError("outOfService", "Cannot create component");
            }
        }
        public async Task<ResponseService> GetComponentByIdAsync(int id)
        {
            try
            {
                var componentModel = await _unitOfWork.Components.GetByIdAsync(id);
                if (componentModel == null)
                {
                    return new ResponseService()
                        .SetSucceeded(false)
                        .SetStatusCode(StatusCodes.Status404NotFound)
                        .AddDetail("message", "Get components failed!")
                        .AddError("notFound", "Cannot found component!");
                }
                var component = _mapper.Map<Component, ComponentModelRequest>(componentModel);
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("data", new { component });
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Get components failed!")
                    .AddError("outOfService", "Cannot get  component!");
            }
        }
        public async Task<ResponseService> GetComponentsAsync()
        {
            try
            {
                var componentsModel = await _unitOfWork.Components.GetAllAsync();
                var components = _mapper.Map<List<Component>, List<ComponentModelRequest>>(componentsModel);
                return new ResponseService()
                    .SetSucceeded(true)
                    .AddDetail("message", "Get components successed")
                    .AddDetail("data", new { components });
            }
            catch
            {
                return new ResponseService()
                    .SetSucceeded(false)
                    .AddDetail("message", "Get components failed")
                    .AddError("error", "Cannot get components");
            }
        }

        
    }   
}
