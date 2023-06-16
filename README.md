### ShareNowBackend


Share your invitations for events with other people.



#### Setup

1. Download private key from [here](https://console.firebase.google.com/project/sharenow-a3665/settings/serviceaccounts/adminsdk)
2. Upadate the constructor of `./Repositories/IBaseRepository.cs` by changing variable `filepath` to point to the location of the downloaded private key.

> Important: Don't add it to Github, use .gitignore !!!!!

#### Frontend Package

https://github.com/alinvelea27/ShareIt


#### Postman collection

Import file `./ShareNow.postman_collection` in Postman

#### How to run

***Backend***

1.) Import in VS Basic
2.) Run `dotnet run`
3.) Starts on port `5073`

***Frontend***

1.) Import in VS Code
2.) Run `npm run start`
3.) Starts on port `3000`