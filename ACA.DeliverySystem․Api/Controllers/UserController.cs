﻿using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Business.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ACA.DeliverySystem_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public UserController(IUserService userService, IMapper mapper, IOrderService orderService)
        {
            _userService = userService;
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserViewModelDTO>> GetAll(CancellationToken token)
        {
            var users = await _userService.GetAll(token);
            return users.Select(x => _mapper.Map<UserViewModelDTO>(x));
        }

        [HttpGet("{userId}")]
        public async Task<UserViewModelDTO> GetById(int userId, CancellationToken token)
        {
            var user = await _userService.GetById(userId, token);
            return _mapper.Map<UserViewModelDTO>(user);
        }

        [HttpGet("by-email/{userEmail}")]
        public async Task<UserViewModelDTO> GetByEmail(string userEmail, CancellationToken token)
        {
            var user = await _userService.GetByEmail(userEmail, token);
            return _mapper.Map<UserViewModelDTO>(user);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserAddModelDTO model, CancellationToken token)
        {
            var user = _mapper.Map<UserAddModel>(model);
            await _userService.Create(user, token);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UserUpdateModelDTO model, CancellationToken token)
        {
            var mappedModel = _mapper.Map<UserUpdateModel>(model);
            var result = await _userService.Update(id, mappedModel, token);
            if (!result.Success)
            {
                if (result.ErrorType == ErrorType.NotFound)
                {
                    return NotFound(result.ErrorMessage);
                }
                return BadRequest(result.ErrorMessage);
            }
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id, CancellationToken token)
        {
            var result = await _userService.Delete(id, token);
            if (!result.Success)
            {
                if (result.ErrorType == ErrorType.NotFound)
                {
                    return NotFound(result.ErrorMessage);
                }
                return BadRequest(result.ErrorMessage);
            }
            return Ok();
        }

        [HttpGet("{userId}/orders")]
        public async Task<IEnumerable<OrderViewModelDTO>> GetUserOrders(int userId, CancellationToken token)
        {
            var orders = await _userService.GetUserOrders(userId, token);
            return orders.Select(o => _mapper.Map<OrderViewModelDTO>(o));

        }

        [HttpPost("addOrder")]
        public async Task<IActionResult> AddOrderInUser([FromQuery] int userId, [FromBody] OrderAddModelDTO model, CancellationToken token)
        {
            var mappedOrder = _mapper.Map<OrderAddModel>(model);
            await _userService.AddOrderInUser(userId, mappedOrder, token);
            return Ok();
        }

    }
}
