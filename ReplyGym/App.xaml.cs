namespace ReplyGym
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var scheduleDatabase = new ScheduleDatabase();
            List<Schedule> schedules = scheduleDatabase.GetAllSchedules();

            if (schedules.Count == 0)
            {
                scheduleDatabase.SaveScheduleData(new Schedule { Day = "Monday", Course = "No selection" });
                scheduleDatabase.SaveScheduleData(new Schedule { Day = "Tuesday", Course = "No selection" });
                scheduleDatabase.SaveScheduleData(new Schedule { Day = "Wednesday", Course = "No selection" });
                scheduleDatabase.SaveScheduleData(new Schedule { Day = "Thursday", Course = "No selection" });
                scheduleDatabase.SaveScheduleData(new Schedule { Day = "Friday", Course = "No selection" });
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}