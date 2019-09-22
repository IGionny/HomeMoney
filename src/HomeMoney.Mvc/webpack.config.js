const path = require('path');
require('webpack');
const {VueLoaderPlugin} = require('vue-loader');
const VuetifyLoaderPlugin = require('vuetify-loader/lib/plugin');
const BundleAnalyzerPlugin = require('webpack-bundle-analyzer')
  .BundleAnalyzerPlugin;

let isDev = process.env.NODE_ENV !==  "production";

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
    "chart.js": "Chart",
    //"moment": "moment",
    //"moment-timezone": "moment-timezone"
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
        //include: path.resolve(__dirname, 'Typescript', "VueJS"),
        //exclude: /node_modules/,
        use: ["cache-loader", "vue-loader"]
      },
      {
        test: /\.tsx?$/,
        //exclude: /node_modules/,
        use: [
          "cache-loader",
          {
            loader: "ts-loader",
            options: {
              appendTsSuffixTo: [/\.vue$/]
            }
          }
        ],
        //include: path.resolve(__dirname, 'Typescript', "VueJS"),
      },
      {
        test: /\.s(c|a)ss$/,
        //exclude: /node_modules/,
        use: [
          'vue-style-loader',
          'css-loader',
          {
            loader: 'sass-loader',
            // Requires sass-loader@^8.0.0
            options: {
              implementation: require('sass'),
              sassOptions: {
                fiber: require('fibers'),
                indentedSyntax: true // optional
              },
            },
          },
        ]
      }
    ]
  },

  devServer: {
    historyApiFallback: true,
    noInfo: true,
    overlay: true
  },
  devtool: 'eval-map',
  plugins: [
    // add this bundle if you want to analyze the size of the generated bundles
    //new BundleAnalyzerPlugin(),
    new VueLoaderPlugin(),
    new VuetifyLoaderPlugin()

  ]
};
