# Project definition
  This was a solo application, where i used C#, React(javascript) as frontend, and PostgreSQL as database, providing users the ability to track and predict sunset times. Leveraging the object-oriented nature of C# for a robust backend, React's declarative UI for a seamless   user experience, and PostgreSQL's ACID compliance for data integrity, the application provides an intuitive interface for users to stay informed about sunset timings, enhancing their awareness of daylight transitions.

# How to start the application
  #Backend
    For backend, there is a docker file. to run the docker file, first simply just build the docker, and after you built it, you can run the following command:
    docker run 
    -e IssueAudience=AUDIENCE_KEY
    -e ConnectionString="Server=host.docker.internal,1433;Database=YOUR_DATABASE_NAME;User Id=USER_NAME;Password=YOUR_STRONG_PASSWORD;MultipleActiveResultSets=true;TrustServerCertificate=True;"
    -e ServiceApiKey="YOUR_DESIRED_API_KEY"
    -e IssueSign=SECRET_KEY
    -e AdminEmail="YOUR_ADMIN_EMAIL"
    -e AdminUserName="ADMIN"
    -e AdminPassword="YOUR_ADMIN_PASSWORD"
    -p 8080:80 YOUR_IMAGE_NAME

  After you ran this command, the backend docker file will work.

# Frontend
  You need a .env file inside the Frontend folder, which contains 2 api key:
  REACT_APP_API_KEY=You can get api key from open-weathermap.org.
  REACT_APP_IMAGE_API_KEY=You can get api key from Pexel.

# Running the application
  After you fisinhed with the previous tasks, you can run the "npm start" command inside the frontend folder.

