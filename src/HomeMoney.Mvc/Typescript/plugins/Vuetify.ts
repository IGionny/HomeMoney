import Vue from 'vue'
import Vuetify from 'vuetify/lib';
import colors from 'vuetify/lib/util/colors'

Vue.use(Vuetify)

const vuetifyOptions = {
  theme: {
    dark: true,
    themes: {
      dark: {
        primary: "#21ba45",
      }
    }
  }
};

export default new Vuetify(vuetifyOptions);
