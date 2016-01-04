namespace StartWithWindows
{
    public class StartManager
    {        
        public static StartManager Current { get; set; }
        public string AppName { get; set; }

        public StartManager(string appName)
        {
            AppName = appName;
        }

        public bool Active
        {
            get { return RegistryController.Exists(AppName); }
        }

        public bool ToggleRegistry()
        {
            if (!Active)
                RegistryController.SetRegistry(AppName);            
            else                        
                RegistryController.RemoveRegistry(AppName);
            return Active;
        }
    }
}