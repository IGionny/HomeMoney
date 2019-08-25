const path = require('path');
require('webpack');
const {VueLoaderPlugin} = require('vue-loader');

let isDev = process.env.NODE_ENV !==  "productionm";

module.exports = {
  entry: {
    App: './Typescript/VueJS/App/App.ts',
    Login: './Typescript/VueJS/Login/Login.ts',
  },
  output: {
    path: path.resolve(__dirname, './wwwroot/VueJS/'),
    publicPath: '/VueJS/',
    filename: '[name].js'
  },
  mode: process.env.NODE_ENV,
  externals: {
    "jquery": "jQuery",
    "vue": 'Vue',
    "vuetify": 'Vuetify',
    "vee-validate": "VeeValidate",
    "chart.js": "Chart",
    "moment": "moment",
    "moment-timezone": "moment-timezone",
    'vuetify': 'vuetify/lib'
  },
  resolve: {
    extensions: ['.ts', '.js', '.vue', '.json'],
    alias: {
      'vue$': 'vue/dist/vue.esm.js'
    }
  },
  module: {
    rules: [
      {
        test: /\.vue$/,
        include: path.resolve(__dirname, 'Typescript', "VueJS"),
        //    exclude: /node_modules/,
        use: ["cache-loader", "vue-loader"]
      },
      {
        test: /\.tsx?$/,
        use: [
          "cache-loader",
          {
            loader: "ts-loader",
            options: {
              appendTsSuffixTo: [/\.vue$/]
            }
          }
        ],
        include: path.resolve(__dirname, 'Typescript', "VueJS"),
        // exclude: /node_modules/,

      },
      {
        test: /\.s(c|a)ss$/,
        use: [
          'vue-style-loader',
          'css-loader',
          {
            loader: 'sass-loader',
            options: {
              implementation: require('sass'),
              fiber: require('fibers'),
              indentedSyntax: true // optional
            }
          }
        ]
      }
    ]
  },

  devServer: {
    historyApiFallback: true,
    noInfo: true,
    overlay: true
  },
  devtool: 'eval',
  plugins: [
    // make sure to include the plugin for the magic
    new VueLoaderPlugin()
  ]
};
