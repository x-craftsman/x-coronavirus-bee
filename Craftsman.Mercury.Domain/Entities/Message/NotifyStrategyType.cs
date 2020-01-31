using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Craftsman.Mercury.Domain.Entities.Message
{
    public enum NotifyStrategyType
    {
        [Description("BACKOFF_RETRY")]
        BackOffRetry,
        [Description("DO_NOT_RETRY")]
        DoNotRetry
    }
}
