﻿using System;
using AutoMapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NewsStories.DAL;
using NewsStories.DAL.Entities;
using NewsStories.DAL.Interfaces;
using NewsStories.Models;

namespace NewsStories.Controllers
{
    public class StoriesController : ApiController
    {
        IUnitOfWork db;

        public StoriesController()
        {
            db = new UnitOfWork();
        }

        //GET: api/Stories
        public IHttpActionResult GetStory()
        {
            var data = db.Story.GetAll();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(new { stories = data });
        }


        // GET: api/Stories/5
        [ResponseType(typeof(Story))]
        public IHttpActionResult GetStory(int id)
        {
            Story data = db.Story.GetByID(id);
            if (data == null)
            {
                return NotFound();
            }

            return Ok(new { story = data });
        }

        // PUT: api/Stories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStory(int id, Story story)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != story.Id)
            {
                return BadRequest();
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!db.Story.StoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Stories
        public IHttpActionResult PostStory([FromBody]Storydto story)
        {
            if (story == null)
            {
                return BadRequest();
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Story addStory = Mapper.Map<Story>(story);
                addStory.PublishedDate = DateTime.Now;
                db.Story.Add(addStory);
                db.SaveChanges();
                return Ok(new { success = true}); ;
            }
            catch (Exception ex)
            {
                return Ok(new { success = false}); ;
            }
        }

        // DELETE: api/Stories/5
        [ResponseType(typeof(Story))]
        public IHttpActionResult DeleteStory(int id)
        {
            Story story = db.Story.GetByID(id);
            if (story == null)
            {
                return NotFound();
            }

            db.Story.Remove(story);
            db.SaveChanges();

            return Ok(story);
        }
    }
}