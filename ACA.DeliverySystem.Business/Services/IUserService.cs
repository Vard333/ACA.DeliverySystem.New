﻿using ACA.DeliverySystem.Business.Models;

namespace ACA.DeliverySystem.Business.Services
{
    public interface IUserService
    {
        Task AddOrderInUser(int userId, OrderAddModel model, CancellationToken token);
        Task Create(UserAddModel user, CancellationToken token);
        Task Delete(int id, CancellationToken token);
        Task<IEnumerable<UserViewModel>> GetAll(CancellationToken token);
        Task<UserViewModel> GetById(int id, CancellationToken token);
        Task<IEnumerable<OrderViewModel>> GetUserOrders(int userId, CancellationToken token);
        Task Update(int id, UserUpdateModel model, CancellationToken token);
    }
}
