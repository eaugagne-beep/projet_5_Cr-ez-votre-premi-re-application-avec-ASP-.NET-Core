# ExpressVoitures

Application web ASP.NET Core MVC permettant de gérer l’inventaire de véhicules d’un concessionnaire.

 Objectifs du projet

- Afficher les véhicules disponibles à la vente
- Ajouter, modifier et supprimer des véhicules (réservé à l’administrateur)
- Gérer les réparations associées aux véhicules  (réservé à l’administrateur)
- Mettre en place une authentification et une autorisation sécurisées
- Utiliser Entity Framework Core 

# Technologies utilisées

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- ASP.NET Identity
- Bootstrap 5
- HTML / CSS


# Modèle de données

- Véhicule
- Marque
- Modèle
- Finition
- Réparation
- Type de réparation


# Sécurité

- Authentification via ASP.NET Identity
- Autorisation par rôles :
  - Admin : accès complet (CRUD véhicules & réparations)
  - Utilisateur : consultation uniquement
- Les actions sensibles sont protégées via `[Authorize(Roles = "Admin")]`

# Fonctionnalités principales

# Utilisateur non connecté / utilisateur simple
- Voir la liste des véhicules
- Voir le détail d’un véhicule
- Visualiser le statut (Disponible / Vendu)

# Administrateur
- Ajouter un véhicule
- Modifier un véhicule
- Supprimer un véhicule
- Ajouter une image et une description
- Gérer les réparations
- Marquer un véhicule comme vendu

#  Base de données

- Créée avec **Entity Framework Core 
- Migrations utilisées pour créer et mettre à jour la base
- SQL Server utilisé comme moteur de base de données

# Installation et lancement

1. Cloner le projet :

https://github.com/eaugagne-beep/projet_5_Cr-ez-votre-premi-re-application-avec-ASP-.NET-Core.git
