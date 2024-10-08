﻿using ACA.DeliverySystem.Data.Models;

namespace ACA.DeliverySystem.Data.Repository
{
    public interface IOrderRepository
    {
        Task<Order> Add(Order order, CancellationToken token);
        Task<OperationResult> AddItemInOrder(int orderId, int itemId, CancellationToken token);
        Task<OperationResult> CancelOrder(int orderId, CancellationToken token);
        Task<OperationResult> Delete(int id, CancellationToken token);
        Task<IEnumerable<Order>> GetAll(CancellationToken token);

        Task<Order> GetById(int id, CancellationToken token);
        Task<OperationResult> OrderCompleted(int orderId, CancellationToken token);
        Task<OperationResult> PayForOrder(int orderId, decimal amount, CancellationToken token);
        Task<OperationResult> RemoveItemFromOrder(int orderId, int itemId, CancellationToken token);
        Task Update(Order order, CancellationToken token);
    }
}