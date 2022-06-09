using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // [AllowAnonymous]
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        // {
        //     var users = await _userRepository.GetUsersAsync();
        //     var returnUsers = _mapper.Map<IEnumerable<MemberDto>>(users);
        //     return Ok(returnUsers);
        // }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var returnUsers = await _userRepository.GetMembersAsync();
            return Ok(returnUsers);
        }

        // [Authorize]
        // [HttpGet("{id}")]
        // public async Task<ActionResult<AppUser>> GetUser(int id)
        // {
        //     return await _db.Users.FindAsync(id);
        // }

        // [HttpGet("{username}")]
        // public async Task<ActionResult<MemberDto>> GetUser(string username)
        // {
        //     var user = await _userRepository.GetUserByUsernameAsync(username);
        //     var returnUser = _mapper.Map<MemberDto>(user);
        //     return returnUser;
        // }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userRepository.GetUserByUsernameAsync(username);

            _mapper.Map(memberUpdateDto, user);
            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }
    }
}
