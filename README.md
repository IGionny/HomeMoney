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
   - [ ]  `<v-text-field>` for money with text-align on the right 
   - [ ]  Finish to handle tags
- [ ] Vue-Router issues
   - [ ] handle active item menù also for sub-routes (like /account/edit/x)

Domain
- [ ] Account: add 'family' concept (this to share the transactions between family components)
    -  [ ] every transaction will inherit the 'family' code from the relative Account

Back-End
- [ ] Asp.Net -> migrate to 3.0
    - [x] Base migration
    - [ ] Replace UseWebpackDevMiddleware that is obsolete
    - [x] Check json serializer (Newtonsoft is not the default one anymore, it's: [System.Text.Json](https://docs.microsoft.com/en-us/dotnet/api/system.text.json?view=netcore-3.0))
- [ ] Data handling
    - [ ] Data validation via FluentValidation
    - [ ] Repository with my MiniORM 'Molto'
    - [ ] 
- [x] Gmail authentication


Functional requirements per Page
--------------------------------

- [ ] Dashboard
    - [ ] Fast add transactions (Expense, Transfer, Income)
    - [ ] Calendar widget
    - [ ] Last transactions
    - [ ] Accounts balance summary
- [ ] Transactions
    - [ ] Grid
    - [ ] Filters
- [ ] Accounts
    - [ ] Grid
    - [ ] Filters?
    - [ ] Edit
        - [ ] From/to date picker hidden by default 
- [ ] Categories
    - [ ] Grid
    - [ ] Filters?
    - [ ] Edit
    - [ ] ? preset of categories? Share with family?
- [ ] Charts
    - ?
- [ ] Compare with CSV
    - [ ] Upload file
    - [ ] read/parse Csv
    - [ ] show page with the csv lines merged with transactions
    - [ ] permit to create a new transaction from csv line 
