using AutoMapper;
using Common.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.ViewModels.Users;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Presentation.Facade.Users;
using Shop.Presentation.Facade.Users.Address;
using Shop.Query.Users.DTOs;

namespace Shop.Api.Controllers
{

    public class UserAddressController : ApiController
    {
        private readonly IUserAddressFacade _userAddressFacade;
        private readonly IMapper _mapper;
        public UserAddressController(IUserAddressFacade userAddressFacade, IMapper mapper)
        {
            _userAddressFacade = userAddressFacade;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<AddressDto?>> GetById(long id)
        {
            var result = await _userAddressFacade.GetById(id);
            return QueryResult(result);
        }

        [HttpGet]
        public async Task<ApiResult<List<AddressDto>>> GetList()
        {
            var result = await _userAddressFacade.GetList(User.GetUserId());
            return QueryResult(result);
        }


        [HttpPost]
        public async Task<ApiResult> AddAddress(AddUserAddressViewModel viewModel)
        {
            var command = _mapper.Map<AddUserAddressCommand>(viewModel);
            command.UserId = User.GetUserId();
            var result = await _userAddressFacade.AddAddress(command);
            return CommandResult(result);

        }

        [HttpDelete("addressId")]
        public async Task<ApiResult> Delete(long addressId)
        {

            var userId = 1;
            var result = await _userAddressFacade.DeleteAddress(new DeleteUserAddressCommand(userId, addressId));
            return CommandResult(result);
        }

        [HttpPut]
        public async Task<ApiResult> EditAddress(EditUserAddressViewModel viewModel)
        {
            var command = _mapper.Map<EditUserAddressCommand>(viewModel);
            command.UserId = User.GetUserId();
            var result = await _userAddressFacade.EditAddress(command);
            return CommandResult(result);
        }


    }
}
