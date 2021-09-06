using JenkinsWebApi.Internal;
using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS1591

namespace JenkinsWebApi.Model
{
    public partial class JenkinsModelRun
    {
        public TimeSpan? DurationTimeSpan { get { return XmlConverter.ToTimeSpan(this.Duration); } }

        public TimeSpan? EstimatedDurationTimeSpan { get { return XmlConverter.ToTimeSpan(this.EstimatedDuration); } }

        public DateTime? TimestampDateTime {  get { return XmlConverter.ToDateTime(this.Timestamp); } }
    }
}
