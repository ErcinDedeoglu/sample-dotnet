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
        [HttpPost, Route("set_info")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetInfo([FromBody] RequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var celebrityRecord = await PrimeApps.RecordGet("celebrities", request.RecordId);
            var instragramUsername = (string) celebrityRecord["instagram_username"];
            //
            HttpClient httpClient = new HttpClient();
            var jsonResult = await httpClient.GetStringAsync("https://www.instagram.com/" + instragramUsername + "/?__a=1");

            var jDto = JObject.Parse(jsonResult);
            var externalUrl = (string)jDto["graphql"]["user"]["external_url"];
            var biography = (string)jDto["graphql"]["user"]["biography"];

            var recordUpdate = new JObject
            {
                ["id"] = request.RecordId,
                ["external_url"] = externalUrl,
                ["biography"] = biography,
            };
            var updateResult = await PrimeApps.RecordUpdate("celebrities", recordUpdate);

            return Ok(new JObject{["result"] = true});
        }

        /// <summary>
        /// Get Users
        /// </summary>  
        ///<response code="200">Successful operation</response>
        ///<response code="400">Invalid request</response>
        ///<returns>List of Users</returns>
        [HttpPost, Route("set_verification")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetVerification([FromBody] RequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var celebrityRecord = await PrimeApps.RecordGet("celebrities", request.RecordId);
            var instragramUsername = (string)celebrityRecord["instagram_username"];
            
            HttpClient httpClient = new HttpClient();
            var jsonResult = await httpClient.GetStringAsync("https://www.instagram.com/" + instragramUsername + "/?__a=1");

            var jDto = JObject.Parse(jsonResult);
            var isVerified = (bool)jDto["graphql"]["user"]["is_verified"];
            var recordUpdate = new JObject
            {
                ["id"] = request.RecordId,
                ["verified"] = isVerified
            };
            var updateResult = await PrimeApps.RecordUpdate("celebrities", recordUpdate);


            return Ok(new JObject { ["result"] = true });
        }

        public class RequestDto
        {
            public int RecordId { get; set; }
        }
    }
}