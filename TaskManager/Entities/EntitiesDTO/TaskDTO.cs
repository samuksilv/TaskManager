using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskManager.Entities.EntitiesDTO {
    /// <summary>
    /// entity to mainpulate tasks
    /// </summary>
    public class TaskDTO {
        /// <summary>
        /// description of task
        /// </summary>
        /// <value></value>
        [JsonProperty ("description"), JsonRequired, Required]
        public virtual string DESCRIPTION { get; set; }

        /// <summary>
        /// date of initiate task
        /// </summary>
        /// <value></value>
        [JsonProperty (@"dateinitial"), JsonRequired, Required]
        public virtual DateTime DATEINITIAL { get; set; }

        /// <summary>
        /// date to waring user
        /// </summary>
        /// <value></value>
        [JsonProperty (@"warningtime"), JsonRequired, Required]
        public virtual DateTime? WARNINGTIME { get; set; }

        /// <summary>
        /// local 
        /// </summary>
        /// <value></value>
        [JsonProperty (@"local"), JsonRequired, Required]
        public virtual string LOCAL { get; set; }

        /// <summary>
        /// sequence of user
        /// </summary>
        /// <value></value>
        [JsonProperty (@"sequser"), JsonRequired, Required]
        public virtual long SEQUSER { get; set; }

    }
}