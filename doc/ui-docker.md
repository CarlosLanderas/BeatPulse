# BeatPulse UI Docker Image

*BeatPulseUI* exist as docker image on [DockerHub](https://hub.docker.com/r/xabarilcoding/beatpulseui/). This image is a simple ASP.NET Core project with the *BeatPulseUI* middleware.

```bash
docker pull xabarilcoding/beatpulseui 
docker run --name ui -p 5000:80 -d beatpulseui:latest
```

You can use the environment variables to configure all properties on *BeatPulseUI*. 

```bash
docker run --name ui -p 5000:80 -e 'BeatPulse-UI:Liveness:0:Name=httpBasic' -e 'BeatPulse-UI:Liveness:0:Uri=http://the-livenes-server-path' -d beatpulseui:latest
```
