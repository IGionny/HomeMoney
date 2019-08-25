<template>
  <v-app id="inspire">
    <v-content>
      <v-container
        class="fill-height"
        fluid
      >
        <v-row
          align="center"
          justify="center"
        >
          <v-col
            cols="12"
            sm="8"
            md="4"
          >
            <v-card class="elevation-12">
              <div class="text-center pt-2 pb-2">
                <div>Login with</div>
                <a href="/account/ExternalSignIn?service=Google">
                  <img src="https://www.google.it/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"/>
                </a>
              </div>
            </v-card>
          </v-col>
        </v-row>
      </v-container>
    </v-content>
  </v-app>
</template>


<script lang="ts">
    import Vue from "vue";
    import Component from "vue-class-component";

    @Component
    export default class LoginVue extends Vue {

    }
</script>