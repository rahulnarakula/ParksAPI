namespace ParkyAPI.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using ParkyAPI.Models;
    using ParkyAPI.Models.Dtos;
    using ParkyAPI.Repository.IRepository;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/Trails")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TrialsController : Controller
    {
        private ITrialRepository _trialRepo;
        private IMapper _mapper;
        public TrialsController(ITrialRepository trialRepo, IMapper mapper)
        {
            _trialRepo = trialRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of national trials
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type=typeof(List<TrialDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetTrials()
        {
            var trials = _trialRepo.GetTrials();
            var trialsDto = new List<TrialDto>();
            foreach (var trial in trials)
            {
                trialsDto.Add(_mapper.Map<TrialDto>(trial));
            }

            return Ok(trialsDto);
        }

        /// <summary>
        /// Get Trial based on id
        /// </summary>
        /// <param name="id">ID of the trial</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetTrial")]
        [ProducesResponseType(200, Type = typeof(List<TrialDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrial(int id)
        {
            var trial = _trialRepo.GetTrial(id);
            if (trial == null)
            {
                return NotFound();
            }
            var trialDto = _mapper.Map<TrialDto>(trial);
            return Ok(trialDto);
        }
        //Check--------------------------------------------------------------------------------
        ///// <summary>
        ///// Get Trials in a park based on park id
        ///// </summary>
        ///// <param name="npId">ID of the park</param>
        ///// <returns></returns>
        //[HttpGet("{npId:int}", Name = "GetTrialsInPark")]
        //[ProducesResponseType(200, Type = typeof(List<TrialDto>))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //[ProducesDefaultResponseType]
        //public IActionResult GetTrialsInPark(int npId)
        //{
        //    var trial = _trialRepo.GetTrialsInNationalPark(npId);
        //    if (trial == null)
        //    {
        //        return NotFound();
        //    }
        //    var trialDto = _mapper.Map<TrialDto>(trial);
        //    return Ok(trialDto);
        //}
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrialDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrial([FromBody] TrialInsertDto trialDto)
        {
            if(trialDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_trialRepo.TrialsExists(trialDto.Name))
            {
                ModelState.AddModelError("", "Trial Exists!");
                return StatusCode(404, ModelState);
            }

            var trial = _mapper.Map<Trial>(trialDto);
            if (!_trialRepo.CreateTrial(trial))
            {
                ModelState.AddModelError("", $"Something went wrong when saving {trial.Name}");
            }
            return CreatedAtRoute("GetTrial", new { id = trial.Id}, trial);
        }

        [HttpPatch("{id:int}", Name = "UpdateTrial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrial(int id, [FromBody] TrialUpdateDto trialDto)
        {
            if (trialDto == null || trialDto.Id != id)
            {
                return BadRequest(ModelState);
            }

            var trial = _mapper.Map<Trial>(trialDto);
            if (!_trialRepo.UpdateTrial(trial))
            {
                ModelState.AddModelError("", $"Something went wrong when updating {trial.Name}");
            }
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteTrial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrial(int id)
        {
            if (!_trialRepo.TrialsExists(id))
            {
                return NotFound();
            }
            var obj = _trialRepo.GetTrial(id);
            if (!_trialRepo.DeleteTrial(obj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting {obj.Name}");
            }
            return NoContent();
        }
    }
}
