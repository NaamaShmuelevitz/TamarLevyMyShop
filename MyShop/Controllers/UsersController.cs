﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities;
using Services;
using DTO;
using AutoMapper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userServices;
        IMapper _mapper;

        public UsersController(IUserService userServices,IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<UserDTO> Get(int id)
        {
          User user = await _userServices.GetById(id);
          return _mapper.Map <User,UserDTO>(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login([FromQuery] string email , [FromQuery] string password)
        {
            User user=await _userServices.LoginUser(email, password);
            if (user != null) { 
                   return Ok(user);}
             return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] PostUserDTO user)
        {
            User NewUser = _mapper.Map<PostUserDTO, User>(user);
            User userRegister =await _userServices.RegisterUser(NewUser);
            if (userRegister != null)
            {
                if(userRegister.FirstName== "Weak password")
                {
                    return NoContent();
                }
                return Ok(userRegister);
            }  
            return BadRequest();
        }

        [HttpPost]
        [Route("password")]
        public int CheckPassword([FromBody] String password)
        {
            return _userServices.CheckPassword(password);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] PostUserDTO userToUpdate)
        {
            User NewUser = _mapper.Map<PostUserDTO, User>(userToUpdate);
            User userUpdate =await _userServices.UpdateUser(id, NewUser);
            if (userUpdate != null)
            {
                if (userUpdate.FirstName == "Weak password")
                {
                    return NoContent();
                }
                return Ok(userUpdate);
            }
            return BadRequest();
        }
    }
}
