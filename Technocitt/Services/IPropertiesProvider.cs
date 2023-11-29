using Microsoft.EntityFrameworkCore;
using Technocitt.AssistantControllers;
using Technocitt.Data;
using Technocitt.Database.Tables;
using Technocitt.mydb;

namespace Technocitt.Services
{

    public interface IPropertiesProvider
    {
        Task<CustomProperty> getProperty(int id);
        Task<CustomProperty> create(string name);
        Task<CustomProperty> update(int ID, string name);
        void delete(int ID);
        Task<IEnumerable<CustomProperty>> getAllProperties();
    }

    public class PropertiesProvider : IPropertiesProvider
    {
        private readonly AppDBContext myDb;

        public PropertiesProvider(AppDBContext myDb)
        {
            this.myDb = myDb;
        }

        public Task<IEnumerable<CustomProperty>> getAllProperties()
        {
            TaskCompletionSource<IEnumerable<CustomProperty>> taskCompletionSource = new TaskCompletionSource<IEnumerable<CustomProperty>>();
            Task.Run(() =>
            {
                IEnumerable<CustomProperty> usr = myDb.TechnoProperties;
                taskCompletionSource.SetResult(usr);
            });
            return taskCompletionSource.Task;

        }


        public Task<CustomProperty> create(string name)
        {
            TaskCompletionSource<CustomProperty> taskCompletionSource = new TaskCompletionSource<CustomProperty>();
            Task.Run(() =>
            {
                //create CustomProperty object with required name in it
                CustomProperty propertyObject = new CustomProperty();
                propertyObject.Name = name;
                myDb.TechnoProperties.Add(propertyObject);
                myDb.SaveChanges();
                taskCompletionSource.SetResult(propertyObject);
            });
            return taskCompletionSource.Task;
        }

        public Task<CustomProperty> update(int ID, string name)
        {
            TaskCompletionSource<CustomProperty> taskCompletionSource = new TaskCompletionSource<CustomProperty>();
            Task.Run(() =>
            {
                CustomProperty? propertyObject = myDb.TechnoProperties.Find(ID);
                if (propertyObject != null)
                {
                    propertyObject.Name = name;
                    myDb.TechnoProperties.Update(propertyObject);
                    myDb.SaveChanges();
                    taskCompletionSource.SetResult(propertyObject);
                }


            });
            return taskCompletionSource.Task;
        }

        public void delete(int ID)
        {
            var entityToDelete = myDb.TechnoProperties.Find(ID);

            if (entityToDelete == null)
            {
                return;
            }

            myDb.TechnoProperties.Remove(entityToDelete);
            myDb.SaveChanges();

        }

        public Task<CustomProperty> getProperty(int id)
        {
            TaskCompletionSource<CustomProperty > taskCompletionSource = new TaskCompletionSource<CustomProperty >();
            Task.Run(() =>
            {
                CustomProperty?  prop = myDb.TechnoProperties
                .FirstOrDefault(u => u.ID==id);
               
                if (prop != null)
                {
                    taskCompletionSource.SetResult(prop);
                }
                else
                {
                    taskCompletionSource.SetResult(null);
                }
                 
            });
            return taskCompletionSource.Task;
        }
    }
}
