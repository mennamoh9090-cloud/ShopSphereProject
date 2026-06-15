using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShopSphere.Application.DTOs;
using ShopSphere.Domain.Entities;
using ShopSphere.Domain.Repositories;

namespace ShopSphere.Application.Services
{
    public class OrderService : IOrderService
    {
        // استخدام _orderRepo هنا عشان يبان عدم اتساق التسمية مع الكومبوننت التانية
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetOrdersWithDetailsAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepo.GetOrderWithDetailsByIdAsync(id);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            
            // بنحط تاريخ إنشاء الطلب علطول
            order.CreatedAt = DateTime.UtcNow;
            order.OrderStatus = "Pending";
            order.PaymentStatus = "Pending";

            await _orderRepo.AddAsync(order);
            
            return _mapper.Map<OrderDto>(order);
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order != null)
            {
                order.OrderStatus = status;
                _orderRepo.Update(order);
            }
        }
    }
}
