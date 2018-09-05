import React from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
// @material-ui/core components
import withStyles from "@material-ui/core/styles/withStyles";
// @material-ui/icons
import Camera from "@material-ui/icons/Camera";
import Palette from "@material-ui/icons/Palette";
import Favorite from "@material-ui/icons/Favorite";
// core components
import Header from "components/Header/Header.jsx";
import Footer from "components/Footer/Footer.jsx";
import Button from "components/CustomButtons/Button.jsx";
import GridContainer from "components/Grid/GridContainer.jsx";
import GridItem from "components/Grid/GridItem.jsx";
import HeaderLinks from "components/Header/HeaderLinks.jsx";
import NavPills from "components/NavPills/NavPills.jsx";
import Parallax from "components/Parallax/Parallax.jsx";
import ReactTable from "react-table";
import profile from "assets/img/faces/me.jpeg";
import axios from 'axios';
import studio1 from "assets/img/examples/studio-1.jpg";
import studio2 from "assets/img/examples/studio-2.jpg";
import studio3 from "assets/img/examples/studio-3.jpg";
import studio4 from "assets/img/examples/studio-4.jpg";
import studio5 from "assets/img/examples/studio-5.jpg";
import work1 from "assets/img/examples/olu-eletu.jpg";
import work2 from "assets/img/examples/clem-onojeghuo.jpg";
import work3 from "assets/img/examples/cynthia-del-rio.jpg";
import work4 from "assets/img/examples/mariya-georgieva.jpg";
import work5 from "assets/img/examples/clem-onojegaw.jpg";
import stockGridStyle from "assets/jss/material-kit-react/views/stockPage.jsx";
import dataTable from "react-table/react-table.css";

class StockGrid extends React.Component {
  constructor(){
    super();
  this.state = {
    Data : []
  }
  this.getData();
}
  getData () {
    axios.get('https://localhost:5001/api/stock')
    .then(res => {
      const data = res.data;
      this.setState ({Data : data});
    });
    }

  render() {
    
    const { classes, ...rest } = this.props;
    const imageClasses = classNames(
      classes.imgRaised,
      classes.imgRoundedCircle,
      classes.imgFluid
    );
    const navImageClasses = classNames(classes.imgRounded, classes.imgGallery);
    

    const columns = [{
        Header: 'Stocks',
        columns: [
            {
                Header: 'Symbol',
                accessor : 'symbol'
            }, 
            {
            Header: 'Company Name',
            id: 'companyName',
            accessor: d => d.companyName
            },
            {
              Header: 'Sector',
              id: 'sector',
              accessor: d => d.sector
              },
            {
              Header: 'Close Price',
              id: 'closePrice',
              accessor: d => d.quotes[0].close
            },
            {
              Header: 'RSI',
              id : 'rsi',
              accessor: d => d.quotes[0].rsi
            }
        ]
    }];
       
    return (
      <div>
        <Header
          color="transparent"
          brand=""
          rightLinks={<HeaderLinks />}
          fixed
          changeColorOnScroll={{
            height: 200,
            color: "white"
          }}
          {...rest}
        />
        <Parallax small filter image={require("assets/img/profile-bg.jpg")} />
        <div className={classNames(classes.main, classes.mainRaised)}>
          <div>
            <div className={classes.container}>
              <GridContainer justify="center">
                <ReactTable 
                data = {this.state.Data}
                columns = {columns}
                defaultPageSize={10}
                className="-striped -highlight" />
              </GridContainer>
            </div>
          </div>
        </div>
        <Footer />
      </div>
    );
  }
}

export default withStyles(stockGridStyle)(StockGrid);
