using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Sample.Models;

namespace Sample.Controllers
{
    [Route("api/instagram")]
    public class InstragramController : BaseController
    {
        /// <summary>
        /// Get Users
        /// </summary>  
        ///<response code="200">Successful operation</response>
        ///<response code="400">Invalid request</response>
        ///<returns>List of Users</returns>
        [HttpPost, Route("set_profile_picture")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetProfilePicture([FromBody] ProfilePictureRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var celebrityRecord = await PrimeApps.RecordGet("celebrities", request.RecordId);
            var instragramUsername = (string) celebrityRecord["instagram_username"];
            //
            HttpClient httpClient = new HttpClient();
            var jsonResult = await httpClient.GetStringAsync("https://www.instagram.com/" + instragramUsername + "/?__a=1");

            var jDto = JObject.Parse(jsonResult);
            var instagramProfilePicture = (string)jDto["graphql"]["user"]["profile_pic_url"];
            var isVerified = (bool) jDto["graphql"]["user"]["is_verified"];
            var recordUpdate = new JObject
            {
                ["id"] = request.RecordId,
                ["profile_picture"] = instagramProfilePicture,
                ["is_verified"] = isVerified
            };
            var updateResult = await PrimeApps.RecordUpdate("celebrities", recordUpdate);
            
            //var fileStream = new MemoryStream(new WebClient().DownloadData(instagramProfilePicture));


            return Ok(true);
        }

        public class ProfilePictureRequest
        {
            public int RecordId { get; set; }
        }
    }
}