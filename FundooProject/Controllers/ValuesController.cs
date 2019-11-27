// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValuesController.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Samir Patil"/>
// --------------------------------------------------------------------------------------------------------------------
namespace FundooProject.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        //// GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //// GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}