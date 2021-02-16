
# mars-rover-navi

Detail documentation is present in the documentation tab of the rover navi web application.

### Here is a quick installation guide:

Installing
Follow these steps to get your development environment set up: (Before Run Start the Docker Desktop)

#### You will need the following tools:

  Visual Studio 2019
  
  .Net Core 3.1 or later
  
  Docker Desktop
  
  Clone the repository
  
  At the src directory, where folders for the projects are present run below command:

#### With Docker File
  dockerbuild -f RoverWebApp/DockerFile-roverwebappimage .
                            
  docker run -it --rm -p 50001:80 --name RoverWebApp roverwebappimage
                            

#### Build Manually
  At the root direction run following

    dotnet build
                            
  Now navigate to webapp directory and run following

  dotnet run
                            
####You can launch Rover Navi as below url:

https://localhost:5001
