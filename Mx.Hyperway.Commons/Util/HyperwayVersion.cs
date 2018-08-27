using System;

namespace Mx.Hyperway.Commons.Util
{
    public class HyperwayVersion
    {
        private static Version version;

        private static Version Current()
        {
            if (version == null)
            {
                version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            }

            return version;
        }

        /// <summary>
        /// the Hyperway version 
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            return Current().ToString();
        }

        /// <summary>
        /// The OS user (from environment) running the build
        /// </summary>
        /// <returns></returns>
        public static string GetUser()
        {
            return Environment.UserName;
        }

        /// <summary>
        /// Describes the build SCM version 
        /// </summary>
        /// <returns></returns>
        public static string GetBuildDescription()
        {
            return Current().Build.ToString();
        }

        /// <summary>
        /// Git SCM version, full format
        /// </summary>
        /// <returns></returns>
        public static string GetBuildId()
        {
            return Current().Build.ToString();
        }

        /// <summary>
        /// The build commit time stamp 
        /// </summary>
        /// <returns></returns>
        public static string GetBuildTimeStamp()
        {
            return Current().Build.ToString();
        }

    }

}
