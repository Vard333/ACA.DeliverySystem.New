﻿using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Data;
using ACA.DeliverySystem.Data.Models;
using AutoMapper;

namespace ACA.DeliverySystem.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task Create(UserAddModel user, CancellationToken token)
        {
            var mappedUser = _mapper.Map<User>(user);
            await _uow.UserRepository.Add(mappedUser, token);
            await _uow.Save(token);
        }
        public async Task<OperationResult> Delete(int id, CancellationToken token)
        {
            var result = await _uow.UserRepository.Delete(id, token);
            if (!result.Success)
            {
                return result;
            }
            await _uow.Save(token);
            return result;
        }

        public async Task<IEnumerable<UserViewModel>> GetAll(CancellationToken token)
        {
            var users = await _uow.UserRepository.GetAll(token);
            return _mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        public async Task<UserViewModel> GetById(int id, CancellationToken token)
        {
            var user = await _uow.UserRepository.GetById(id, token);
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<OperationResult> Update(int id, UserUpdateModel model, CancellationToken token)
        {
            var oldUser = await _uow.UserRepository.GetById(id, token);
            if (oldUser == null)
            {
                return OperationResult.Error($"User with id {id} not found.", ErrorType.NotFound);
            }
            oldUser.Name = model.Name;
            oldUser.SureName = model.SureName;
            await _uow.UserRepository.Update(oldUser, token);
            await _uow.Save(token);
            return OperationResult.Ok();
        }

        public async Task<IEnumerable<OrderViewModel>> GetUserOrders(int userId, CancellationToken token)
        {
            var user = await _uow.UserRepository.GetById(userId, token);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with Id {userId} not found");
            }
            var orders = await _uow.UserRepository.GetUserOrders(userId, token);
            return orders.Select(o => _mapper.Map<OrderViewModel>(o));

        }

        public async Task AddOrderInUser(int userId, OrderAddModel model, CancellationToken token)
        {
            var user = await _uow.UserRepository.GetById(userId, token);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            var mappedOrder = _mapper.Map<Order>(model);
            var order = await _uow.OrderRepository.Add(mappedOrder, token);
            await _uow.UserRepository.AddOrderInUser(userId, order, token);
            await _uow.Save(token);

        }


    }
}
