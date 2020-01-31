using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.LaunchPad.Domain.Entities
{
    /// <summary>
    /// Consumer Application 
    /// </summary>
    public enum ConsumerApp
    {
        Unknow = 0,
        /// <summary>
        /// LaunchPad
        /// </summary>
        LaunchPad = 1000,
        /// <summary>
        /// SCM TMS
        /// </summary>
        ScmTms = 1001,
        /// <summary>
        /// SCM WMS
        /// </summary>
        ScmWms = 1002,
        /// <summary>
        /// SCM OMS
        /// </summary>
        ScmOms = 1003,
        /// <summary>
        /// SCM AMS
        /// </summary>
        ScmAms = 1004
    }
}
