## Sample PrimeApps Project (.NET Core)

This sample demonstrates how to develop a PrimeApps project using .NET Core.

### Prerequisites

Before running this sample locally, you need to install and start PrimeApps Runtime. Please follow [this guide](https://github.com/primeapps-io/omnibus) to install PrimeApps Runtime. Then follow the steps below.

### Setup

1. [Fork this repository](#1-fork-this-repository)
2. [Develop your code](#2-develop-your-code)
3. [Build Docker image](#3-build-docker-image)
4. [Publish Docker container](#4-publish-docker-container)

#### 1. Fork this repository
In the top-right corner of the page, click Fork.

> <img src="http://file.primeapps.io/assets/github/fork.jpg" alt="drawing" width="350"/>


#### 2. Develop your code
You can clone your forked repository to your local machine now. Then develop your APIs, scripts and, components. Commit and push your code.

#### 3. Build Docker image
You can build and push Docker image to GitHub Packages with simple steps. We've created ready-to-use Dockerfile and Github Build Actions for you.

##### 3.1. Click "Actions" tab in your forked repository.
> <img src="http://file.primeapps.io/assets/github/actions_tab.jpg" alt="drawing" width="350"/>

##### 3.2. Click "I understand my workflows, go ahead and run them" button.
> <img src="http://file.primeapps.io/assets/github/actions_enable.jpg" alt="drawing" width="600"/>

##### 3.3. Enter "Releases" menu. It's a submenu under "Code" tab.
> <img src="http://file.primeapps.io/assets/github/releases_tab.jpg" alt="drawing" width="400"/>

##### 3.4. Click "Create a new release" button.
> <img src="http://file.primeapps.io/assets/github/releases_new.jpg" alt="drawing" width="500"/>

##### 3.5. Enter v1.0 in "Tag version" input and click "Publish release" button. After that, a build is started automatically. You can see progress in "Actions" menu. It will be finished in about 2 minutes.
> <img src="http://file.primeapps.io/assets/github/releases_tag.jpg" alt="drawing" width="400"/>

> <img src="http://file.primeapps.io/assets/github/releases_button.jpg" alt="drawing" width="200"/>


#### 4. Publish Docker container
After the build is finished, your Docker image will be ready in "Packages" menu which is a submenu under "Code" tab. You can find the image path there. Something like that:

```bash
docker pull docker.pkg.github.com/username/sample-dotnet/primeapps-sample:1.0.0
```

You can now create a container in PrimeApps Studio using this information.
