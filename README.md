## Sample PrimeApps Container Project (.NET Core)

This sample demonstrates how to create a PrimeApps Container using .NET Core and Github Actions.

### Prerequisites

Before running this sample locally, you need to install and start PrimeApps Runtime. Please follow [this guide](https://github.com/primeapps-io/omnibus) to install PrimeApps Runtime. Then follow the steps below.

### Setup

1. [Fork this repository](#1-fork-this-repository)
2. [Code your APIs, scripts and, components](#2-code-your-apis-scripts-and-components)
3. [Build Docker image](#3-build-docker-image)
4. [Create a Container on PrimeApps Studio](#4-create-a-container-on-primeapps-studio)

#### 1. Fork this repository
In the top-right corner of the page, click Fork.

> <img src="http://file.primeapps.io/assets/github/fork.png" alt="drawing" width="350"/>


#### 2. Code your APIs, scripts and, components
Clone the forked repository to your local machine. Create your APIs, scripts and, components. Commit and push to this repository.

#### 3. Build Docker image
You can build and push the Docker image to GitHub Packages with simple steps. We've created ready-to-use Dockerfile and Github Build Actions for you.

##### 3.1. Click the "Actions" tab in the forked repository.
> <img src="http://file.primeapps.io/assets/github/actions_tab.png" alt="drawing" width="350"/>

##### 3.2. Click the "I understand my workflows, go ahead and run them" button.
> <img src="http://file.primeapps.io/assets/github/actions_enable.png" alt="drawing" width="600"/>

##### 3.3. Click the "Create a new release" button. It's under the "Code" tab.
> <img src="http://file.primeapps.io/assets/github/releases_new.png" alt="drawing" width="140"/>

##### 3.4. First, enter v1.0.0 in the "Choose a tag" input and click the "Create new tag". Then click the "Publish release" button. A build is started automatically. You can see progress in the "Actions" menu. It will be finished in about 2 minutes.
> <img src="http://file.primeapps.io/assets/github/releases_tag.png" alt="drawing" width="300"/>

> <img src="http://file.primeapps.io/assets/github/releases_button.png" alt="drawing" width="250"/>


#### 4. Create a Container on PrimeApps Studio
After the build is finished, your Docker image will be ready in "Packages" menu which is under "Code" tab. You can find the image information there.

> <img src="http://file.primeapps.io/assets/github/pull_command.png" alt="drawing" width="600"/>


You can now create a container on PrimeApps Studio now. When you create a container on PrimeApps Studio copy and paste the docker pull command to Image input.  
