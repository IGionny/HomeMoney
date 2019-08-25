<template>
  <v-app id="inspire">
    <NavigationDrawer :drawer="drawer"></NavigationDrawer>
    <!--
    <v-navigation-drawer
      v-model="drawer"
      app
      clipped
    >
      <v-list dense>
        <v-list-item @click="">
          <v-list-item-action>
            <v-icon>dashboard</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Dashboard</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item @click="">
          <v-list-item-action>
            <v-icon>settings</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>Settings</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
-->
    <v-app-bar
      app
      clipped-left
    >
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>
        <i class="fas fa-wallet"></i> Home Money
      </v-toolbar-title>

      <v-flex xs3 offset-xs9 align-end text-right>


        <v-menu offset-y>
          <template v-slot:activator="{ on }">

            <v-chip
              class="ma-2"
              color="primary"
              label
              v-on="on"
            >
              <v-icon left>mdi-account-circle-outline</v-icon>
              {{UserContext.Name}}
            </v-chip>
          </template>
          <v-list>
            <v-list-item>
              <a href="/Account/Logout">Logout</a>
            </v-list-item>
          </v-list>
        </v-menu>

      </v-flex>

    </v-app-bar>

    <v-content>
      <v-container
        class="fill-height"
        fluid
      >
        <v-row
          align="center"
          justify="center"
        >


          <HomeIndex></HomeIndex>


        </v-row>
      </v-container>
    </v-content>

    <v-footer app>
      <span>&copy; 2019</span>
    </v-footer>
  </v-app>
</template>

<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";
    import HomeIndex from "./Home/Index.vue";
    import {IUserContext} from "../../@types/IUserContext";
    import NavigationDrawer from "./components/NavigationDrawer.vue";

    @Component({
        components: {NavigationDrawer, HomeIndex}
    })
    export default class AppVue extends Vue {

        drawer: boolean = false;

        UserContext: IUserContext = {
            Name: "",
            Email: ""
        };

        mounted() {
            let el = document.getElementById("UserContext");

            if (el !== null) {
                this.UserContext = JSON.parse(el.innerHTML) as IUserContext;
            }


        }

    }
</script>