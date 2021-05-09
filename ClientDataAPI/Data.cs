namespace Client.DataAPI
{
    public static class Data
    {
        private static Repository repository;

        private static Repository CreateRepository()
        {
            repository = new Repository();
            
            return repository;
        }

        public static Repository GetRepository()
        {
            if (repository is null)
            {
                repository = CreateRepository();
            }
            return repository;
        }

        public static void ClearContext()
        {
            DbContext.ClearContext();
            repository = null;
        }
    }
}
