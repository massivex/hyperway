using System;

namespace Mx.Hyperway.Commons.Util
{
    public class HyperwayVersion
    {
        private static Version version;

        private static Version GetVersion()
        {
            if (version == null)
            {
                version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            }

            return version;
        }

        /**
         * The Oxalis version, taken from the POM
         */
        public static String getVersion()
        {
            return GetVersion().ToString();
        }

        /**
         * The OS user (from environment) running the build
         */
        public static String getUser()
        {
            return System.Environment.UserName;
        }

        /**
         * Describes the build SCM version
         */
        public static String getBuildDescription()
        {
            return GetVersion().Build.ToString();
        }

        /**
         * Git SCM version, full format
         */
        public static String getBuildId()
        {
            return GetVersion().Build.ToString();
        }

        /**
         * The build commit time stamp
         */
        public static String getBuildTimeStamp()
        {
            return GetVersion().Build.ToString();
        }

    }

}
