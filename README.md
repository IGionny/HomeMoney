HomeMoney
=========

*** STILL IN ALPHA ***

HomeMoney is an Asp.Net Core WebApp with a Front-End written in VueJs (Vuetify) and Typescript to manage the moneys of a simple
family.
Freely inspired by MoneyTracker project https://demo.moneytracker.cc

The idea is to have a web app (mobile/wife friendly) to install into a Raspberry3+ @ home.\
The data will be persisted in a postgresql database or in a SqlLite.\
Every hours a backup system will sync data with a file in a customizable G-Drive space.



Current tasks
-------------

Front-End

- [ ] Vuetify issues 
   - [ ]  `<v-text-field>` for momey with text-align on the right 
   - [ ]  Finish to handle tags
- [ ] Vue-Router issues
   - [ ] handle active item menù also for sub-routes (like /account/edit/x)
      

Back-End
- [ ] Data handling
    - [ ] Data validation via FluentValidation
    - [ ] Repository with my MiniORM 'Molto'
    - [ ] 
- [x] Gmail authentication