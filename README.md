# E-commerce.Net.React

 "E-commerce" as the name implies is an server for an online store. 
https://e-commerce-boutique.netlify.app

Tech stack used:
- .Net 6 for the backend 
     - User authorization and authentication
     - RESTful API
- ReacJS for the frontend 
- Redux-Toolkit
- PostgreSQL for the database 
- AWS S3 to serve images

# Endpoints backend

Auth endpoints:
  - /api/Auth/v1/register/newUser
  - /api/Auth/v1/login

User endpoints:
  - /api/User/v1/getAllUsers
  - /api/User/v1/get/userById/{id}
  - /api/User/v1/update/userById/{id}
  - /api/User/v1/delete/userById/{id}  
  
Product endpoints:
  - /api/Product/v1/create/newProduct
  - /api/Product/v1/get/allProducts
  - /api/Product/v1/get/productById/{id}
  - /api/Product/v1/update/productById/{id}
  - /api/Product/v1/delete/productById/{id}

Role endpoint:
  - /api/Role/v1/create/newRole/nameRole
  - /api/Role/v1/get/roleById/{id}
  - /api/Role/v1/get/allRoles
  - /api/Role/v1/update/roleById/{id}
  - /api/Role/v1/delete/roleById/{id}
  
CategoryProduct endpoints:
  - /api/CategoryProduct/v1/add/newCategoryProduct
  - /api/CategoryProduct/v1/get/allCategory
  
  ![swagger](https://user-images.githubusercontent.com/63923347/207935445-cfc01943-f246-4f23-a49f-f128e3959841.png)
![login](https://user-images.githubusercontent.com/63923347/191663942-1e342d69-79a6-482e-83ed-d274a129c589.png)
![home](https://user-images.githubusercontent.com/63923347/191663927-ec59e2d4-d40e-45cb-9dbe-af0b95f880ed.png)
![clothes](https://user-images.githubusercontent.com/63923347/191663954-8f01c30a-e4b1-467c-8118-1743fade9728.png)
![details product](https://user-images.githubusercontent.com/63923347/191663961-6e2cba7e-51ba-420d-9e92-a1414d314aa9.png)
![logout](https://user-images.githubusercontent.com/63923347/191663974-03a31e19-c9a1-4e82-9e73-270941605804.png)
![cart](https://user-images.githubusercontent.com/63923347/191663967-dac9853c-6140-4f03-8134-0c707f1045f1.png)
![dashboard admin](https://user-images.githubusercontent.com/63923347/191664141-840d2ca2-ca81-4b63-a81e-874486ce14ab.png)
![Create New Product](https://user-images.githubusercontent.com/63923347/208293959-54acb399-46a9-44c8-95fa-9eaab394fc05.png)
