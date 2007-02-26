/* 
* Copyright 2004-2005 OpenSymphony 
* 
* Licensed under the Apache License, Version 2.0 (the "License"); you may not 
* use this file except in compliance with the License. You may obtain a copy 
* of the License at 
* 
*   http://www.apache.org/licenses/LICENSE-2.0 
*   
* Unless required by applicable law or agreed to in writing, software 
* distributed under the License is distributed on an "AS IS" BASIS, WITHOUT 
* WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
* License for the specific language governing permissions and limitations 
* under the License.
* 
*/

/*
* Previously Copyright (c) 2001-2004 James House
*/
using System;
using Quartz.Util;

namespace Quartz.Xml
{
	/// <summary> 
	/// Wraps a <code>ICalendar</code>.
	/// </summary>
	/// <author><a href="mailto:bonhamcm@thirdeyeconsulting.com">Chris Bonham</a></author>
	[Serializable]
	public class CalendarBundle : ICalendar
	{
		/// <summary>
		/// Gets or sets the name of the calendar.
		/// </summary>
		/// <value>The name of the calendar.</value>
		public virtual string CalendarName
		{
			get { return calendarName; }
			set { calendarName = value; }
		}

		/// <summary>
		/// Gets or sets the name of the class.
		/// </summary>
		/// <value>The name of the class.</value>
		public virtual string ClassName
		{
			get { return className; }
			set
			{
				className = value;
				CreateCalendar();
			}
		}

		/// <summary>
		/// Gets or sets the calendar.
		/// </summary>
		/// <value>The calendar.</value>
		public virtual ICalendar Calendar
		{
			get { return calendar; }
			set { calendar = value; }
		}

		public virtual bool Replace
		{
			get { return replace; }
			set { replace = value; }
		}

		/// <summary>
		/// Gets or sets a description for the <code>Calendar</code> instance - may be
		/// useful for remembering/displaying the purpose of the calendar, though
		/// the description has no meaning to Quartz.
		/// </summary>
		/// <value></value>
		public virtual string Description
		{
			get { return calendar.Description; }
			set { calendar.Description = value; }
		}

		protected internal string calendarName;
		protected internal string className;
		protected internal ICalendar calendar;
		protected internal bool replace;

		/// <summary>
		/// Set a new base calendar or remove the existing one.
		/// </summary>
		/// <value></value>
		public virtual ICalendar CalendarBase
		{
			get { return calendar.CalendarBase; }
			set
			{
				if (value is CalendarBundle)
				{
					value = ((CalendarBundle) value).Calendar;
				}
				calendar.CalendarBase = value;
			}
		}

		public virtual bool IsTimeIncluded(DateTime timeStamp)
		{
			return calendar.IsTimeIncluded(timeStamp);
		}

		public virtual DateTime GetNextIncludedTime(DateTime timeStamp)
		{
			return calendar.GetNextIncludedTime(timeStamp);
		}

		protected internal virtual void CreateCalendar()
		{
			Type type = Type.GetType(className);
			Calendar = (ICalendar) ObjectUtils.InstantiateType(type);
		}
	}
}