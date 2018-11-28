[logo]: https://zoobox.azurewebsites.net/images/logo.png
[login]: https://i.imgur.com/8cetyyC.jpg
[createUser]: https://i.imgur.com/fJpWP6Y.jpg
[addTypeRole]: https://i.imgur.com/wZygQ0F.jpg
# ![logo]



O sistema de informação `zoobox` tem como objectivo o auxílio dos processos de negócio realizados pelo albergue escolhido. Uma vez que grande parte destes processos consistem no registo de informação, este sistema irá focar-se na persistência de dados e manipulação dos mesmos. Para além disto, o sistema também prestará auxílio em algumas tarefas, possuindo ferramentas como um sistema de notificação, facilitando a realização das mesmas. 

Assim, o sistema prestará auxílio nas principais secções de gestão de dados, como: 

- Gestão de stock 

- Gestão de funcionários e voluntários 

- Gestão de adoções 

- Gestão de animais 

 Tendo em conta que o albergue escolhido não possui nenhum sistema de informação, a simplicidade e facilidade da manipulação de dados será um fator a considerar no desenho do mesmo. Iram existir 3 utilizadores com permissões diferentes: responsável do canil(administrador), funcionário e voluntário. 

Em suma, este sistema pretende substituir e melhorar os métodos de gerenciamento que estão a ser utilizados por este albergue neste momento. 

## Utilização

Poderá testar a aplicação em  [zoobox.azurewebsites.net](http://zoobox.azurewebsites.net) com o seguinte login.

```bash
username: admin@zoobox.com
password: 123456_Abc
```
![login]

Para criar um novo utilizador no sistema tem de ser um administrador do mesmo, depois ir ao menu de navegação e `Utilizadores > Registar`.
Preencher os dados pretendidos para esse utilizador e fica criado.

![createUser]

Ir ao menu de navegação e `Utilizadores > Registar` se quiser mudar o tipo de um utilizador basta escrever o email do mesmo e colocar o tipo pretendido.

![addTypeRole]

### Funcionalidades implementadas
#### 1º Sprint

```bash
 - O sistema deverá permitir ao responsável do canil criar uma conta de utilizador. 

 - Deverá permitir ao responsável do canil/funcionário/voluntário editar os dados da sua conta de utilizador(username,password).  

 - O sistema deverá permitir ao responsável do canil/funcionário/voluntário se autenticar no sistema. 
 
 - O sistema deverá permitir ao responsável do canil associar uma conta de utilizador a uma ficha de funcionário. 

 - O sistema deverá permitir ao utilizador recuperar a sua password.

```
####


## Grupo X

André Silva (100221022) | Diogo Paulino (110221050) | Tiago Alves (150221088) 