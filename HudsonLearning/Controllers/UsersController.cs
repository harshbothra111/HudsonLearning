using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HudsonLearning.DTOs;
using HudsonLearning.Extensions;
using HudsonLearning.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudsonLearning.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly BlobServiceClient _blobServiceClient;

        public UsersController(IUserRepository userRepository, IMapper mapper, BlobServiceClient blobServiceClient)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _blobServiceClient = blobServiceClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var members = await _userRepository.GetMembersAsync();
            return Ok(members);
        }

        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            _mapper.Map(memberUpdateDto, user);
            _userRepository.Update(user);
            if (await _userRepository.SaveAllSync()) return NoContent();
            return BadRequest("Failed to update user");
        }
        [HttpPut("add-photo")]
        public async Task<ActionResult<string>> AddPhoto(IFormFile file)
        {
            if (file == null) return BadRequest("Photo not attached");
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            var fileName = User.GetUsername() + "_" + Guid.NewGuid() + Path.GetExtension(file.FileName);
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient("profile-photos");
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
            var httpHeaders = new BlobHttpHeaders
            {
                ContentType = file.ContentType
            };
            await blobClient.UploadAsync(file.OpenReadStream(), httpHeaders);
            user.PhotoUrl = blobClient.Uri.AbsoluteUri;
            _userRepository.Update(user);
            if (await _userRepository.SaveAllSync()) return Ok(user.PhotoUrl);
            return BadRequest("Problem adding photo");
        }
    }
}
