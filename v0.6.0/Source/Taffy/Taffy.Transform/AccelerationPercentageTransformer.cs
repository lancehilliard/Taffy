using System;
using Taffy.Configuration;

namespace Taffy.Transform {
    public interface IAccelerationPercentageTransformer {
        int ParseAccelerationPercentage(string percent);
    }

    public class AccelerationPercentageTransformer : IAccelerationPercentageTransformer {
        public int ParseAccelerationPercentage(string percent) {
            int accelerationPercentage;
            try {
                accelerationPercentage = Int32.Parse(percent);
            }
            catch (Exception exception) {
                accelerationPercentage = Constants.AccelerationPercentageDefault;
            }
            return accelerationPercentage;
        }
    }
}