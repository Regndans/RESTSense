﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTSense.Model;

namespace RESTSense.Managers
{
    public class TimerManager
    {
        private readonly PirContext _context;

        public TimerManager(PirContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Method to get all from timer
        /// </summary>
        /// <returns></returns>
        public List<TimerModel> GetAll()
        {
            return _context.ActiveTime.ToList();
        }
        /// <summary>
        /// Method to update the timer
        /// </summary>
        /// <param name="v"></param>
        /// <param name="newTime"></param>
        /// <returns></returns>
        public TimerModel UpdateTimer(int v, TimerModel newTime)
        {
            TimerModel toChange = _context.ActiveTime.Find(v);
            if (toChange == null) return null;
            toChange.ActiveEnd = newTime.ActiveEnd;
            toChange.ActiveStart = newTime.ActiveStart;
            toChange.TimerActive = newTime.TimerActive;
            _context.SaveChanges();
            return toChange;
        }
    }
}
