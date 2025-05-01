namespace GUI_DB
{
    public class GlobalVariable
    {
        public static string CurrentlyLoggedIN;
        public static int CurrentMovie;
        public static int CurrenthallId;

        public static void setMovie(int Movie)
        {
            CurrentMovie = Movie;
        }
        public static void setCurrentlyLoggedIN(string Email)
        {
            CurrentlyLoggedIN = Email;
        }

        public static string getCurrentlyLoggedIN()
        {
            return CurrentlyLoggedIN;
        }

        public static int getCurrentMovie()
        {
            return CurrentMovie;
        }
        public static int getCurrentHallId()
        {
            return CurrenthallId;
        }
        public static void setCurrentHallId(int hallId)
        {
            CurrenthallId = hallId;
        }
    }
}