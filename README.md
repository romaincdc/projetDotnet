## Introduction

Bonjour,
Voici le projet concernant la réalisation d'un Back-end en Dotnet ainsi que d'un FRONT.
Dans le Repo git vous allez retrouver un dossier où se trouvent le front et un autre dossier où se trouve le Back.

## Techno Utilisé

- Pour le front, c'est un fichier HTML classique avec du JavaScript pour connecter l'api.
- L'API a été réalisée en Dotnet 7.
- La base de données utilisée est une BDD MySQL.

## What's contained in this project

Le projet Dispose d'un Swagger qui contient toute les requêtes disponibles avec l'API :

Attendance:

- Possibilité de voir tous les attendances enregistrer sur la BDD
- On peut ajouter une attendance
- On peut voir les attendance pour 1 seul employer en indiquant son ID.

DEPARTMENTS:

- Possibilité de voir tous les DEPARTMENTS
- On peut ajouter un department
- On peut voir le deparments par leur nom ou par leur ID.
- Possibilité de modifier un department
- On peut en supprimer 1 aussi.

EMPLOYEES:

- Possibilité de voir tous les Employees
- On peut ajouter un employee
- On peut rechercher par le nom ou l'ID.
- Possibilité d'update un employee
- Suppression d'un employee
- Possibilité d'ajouter ou de supprimer un department lié a un employé

LEAVE REQUEST:

- Possibilité de voir l'ensemble des leaves requests
- On peut créer un leave Request si on le souhaite.
- Modification du status de la leave request possible
- Possibilité de rechercher les leave request par id ou voir tous ceux liés a un employé.


Fonctionnement du Front:

On voit l'ensemble des departments,employee,attendance et leaveRequest
On peut ajouter ou supprimer un employee ou department
On peut modifier un employee ou un department
On peut lié un employee a un department
On peut créer et modifier un leave Request

Pour pouvoir utilisé le Front avec l'API suite à des problèmes de CORS POLICY qui ne sont pas corrigé étant donné que c'est un fichier HTML local, il faut ouvrir le fichier HTML dans un google chrome ou les sécurités web sont désactivé. La commande a effectué est la suivante :

open -n -a /Applications/Google\ Chrome.app/Contents/MacOS/Google\ Chrome --args --user-data-dir="/tmp/chrome_dev_test" --disable-web-security