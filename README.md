# Space-Invaders

## Description du projet 
Ce projet est la réalisation du jeu *Space-Invaders* avec plusieurs ajouts.
La piste suivi est la piste bleu, autonomie de création du jeu.
Les fonctionnalités de bases sont toutes développés et de nombreux ajouts ont été 
- Bonus
  * Bonus de vitesse d'attaque
  * Bonus de puissance d'attaque
  * Bonus de heal
  * Bonus consommable de tir puissant
- Système d'invisincibilité du vaisseau après un dégat
- Système de score
- Particules d'explosion
- Menu de sélection de mode de jeu
- Mode de jeu "manic shooter"

Le mode Manic Shooter est un nouveau mode de jeu qui change complètement l'aspect du jeu.
Pour plus de détails, voici une vidéo qui explique l'origine et les aspects de ce type de jeu: https://www.youtube.com/watch?v=q02XCVp6Ww8
Les principes suivis pour ce mode sont le fait qu'il y a peu d'ennemis et énormément de projectiles qui sont difficiles à esquiver.

Dans le projet, le mode apporte:
 - Possibilités de se déplacer en haut et en bas
 - Mode de jeu sans fin avec un système de vagues d'ennemis
 - Les invaders sont autonomes
 - Le système de projectiles ainsi que les calculs sur les vecteurs sont beaucoup plus complexes pour permettre des patterns projectiles très originaux ![](Images/screenBullets.gif)
 - De nombreux boss avec des comportements différents, et des attaques uniques ![](Images/screenSmartBoss.gif)![](Images/screenSpammer.gif)

## Structure du programme
![Schéma UML](model.jpg)
Voici un diagramme UML non exaustif du projet, il présente les aspects principaux et la hierarchie des classes.

Le programme est lancé par **Program**, qui lance le **Menu** de sélection de mode pour lancer la partie.

On peut voir que les 2 classes principales sont **Game** et **GameObject**.
- La classe **Game** s'occupe de de la boucle principal du jeu, et de la gestion des **GameObjects**.
- La classe **GameObject** est le parent de tous les objets du jeu, elle contient les attributs et méthodes qui sont communes à tous les objets, comme **Update()**, **Kill()**, ou encore **Position** et **Speed**

La classe **Ship** (le vaisseau du joueur) et la classe **Invader** sont toutes deux des filles de **LivingEntity**, qui sert à gérer les points de vie. Un **Laser** n'est pas une **LivingEntity** car il est détruit instantanément à chaque collision et ne nécessite pas de vie.

Les classes utilitaires ne sont pas reliés avec les éléments qui les utilisent pour ne pas alourdir le schéma avec des flèches partout, elles sont nottement composés de **TimedAction** et de **Vecteur2D** qui sont très utilisés, et de méthodes d'extensions sur les **List** ou sur **Random**.
L'interface **IImage** est aussi sous-représenté sur ce schéma pour la lisibilité.

## Problèmes rencontrés
### Gestion des trajectoires des balles
TODO
### Gestion des vagues
TODO
### Test et Debug
TODO 

> Le barême ci-dessous est donné à titre indicatif et peut évoluer.
> Le rapport est noté sur 20 : les éléments de notations sont:
    > la qualité de la rédaction (y compris présententation, orthographe et grammaire)
    > la présentation du sujet
    > l’explication de la solution et
    > l’analyse des problèmes rencontrés.

> Le rapport fera au maximum 5 pages. Il devra contenir :
> une description succincte du problème,
> une description de la structure de votre programme permettant de comprendre son fonctionnement,

> une présentation des problèmes soulevés par les tests : analyse de l’origine du problème, de la solution trouvée ou des > > > idées de solutions (si le problème n’a pas été résolu).
