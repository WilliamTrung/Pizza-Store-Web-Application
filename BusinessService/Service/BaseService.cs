using ApplicationCore.Models2;
using ApplicationCore.Repository;
using AutoMapper;
using BusinessService.IService;
using BusinessService.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Service
{
    public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        public PizzaStoreContext _context;
        public IMapper _mapper;
        IGenericRepository<TEntity> _repository;
        public BaseService(PizzaStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _repository = new GenericRepository<TEntity>(_context);
        }
        public virtual async Task<TDto> Create(TDto dto)
        {
            if (dto != null)
            {
                var entity = _mapper.Map<TEntity>(dto);
                try
                {
                    entity = await _repository.Create(entity);
                    dto = _mapper.Map<TDto>(entity);
                }
                catch
                {
                    dto = null;
                }
            }
            return dto;
        }

        public async Task<bool> Delete(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            if (entity != null)
            {
                if(await _repository.Delete(entity))
                    return true;
                return false;
            }
            else
            {
                return false;
            }
        }

        public virtual void DisableSelfReference(ref TEntity entity)
        {
            //throw new NotImplementedException();
        }

        public async Task<TDto> Update(Expression<Func<TEntity, bool>> filter, TDto dto)
        {
            if (dto != null)
            {
                var entity = _mapper.Map<TEntity>(dto);
                try
                {
                    entity = await _repository.Update(filter, entity);
                    if(entity != null)
                    {
                        dto = _mapper.Map<TDto>(entity);
                    } else
                    {
                        dto = null;
                    }
                }
                catch
                {

                    dto = null;
                }

            }
            return dto;
        }

        public async Task<IEnumerable<TDto>> GetDTOs(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null, PagingRequest? paging = null)
        {
            if (paging == null)
                paging = new PagingRequest();
            var list_entities_raw = (await _repository.Get(filter, includeProperties)).Skip((paging.PageIndex - 1) * paging.PageSize)
               .Take(paging.PageSize);

            var list_entities = new List<TEntity>();
            for (int i = 0; i < list_entities_raw.Count(); i++)
            {
                var entity = list_entities_raw.ElementAt(i);
                DisableSelfReference(ref entity);
                list_entities.Add(entity);
            }
            if (list_entities != null && list_entities.Count() > 0)
            {
                List<TDto> list_dto = new List<TDto>();
                foreach (var entity in list_entities)
                {
                    var dto = _mapper.Map<TDto>(entity);
                    list_dto.Add(dto);
                }
                return list_dto;
            }
            else
            {
                return new List<TDto>();
            }
        }
    }
}
