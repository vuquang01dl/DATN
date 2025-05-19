import React, { Component } from 'react';
import { connect } from 'react-redux';
import * as actions from './../../actions/index';

class BookingForm extends Component {

    constructor(props) {
        super(props);
        this.state = {
            id : '',
            name : '',
            status : false
        };
    }

    onExitForm = () => {
        this.props.onCloseForm();
    }

    render() {
        return (
            <div className="panel panel-warning">
                <div className="panel-heading">
                    <h3 className="panel-title">
                        BOOKING TOUR
                        <span className="fa fa-times-circle text-right floatRight" onClick={this.onExitForm}></span>
                    </h3>
                </div>
                <div className="panel-body">
                    <div id="booking" className="section">
                        <div className="section-center">
                            <div className="container">
                                <div className="row">
                                    <div className="booking-form">
                                        <div className="form-header">
                                            <h1>Book Tour</h1>
                                        </div>
                                        <form>
                                            <div className="row">
                                                <div className="col-sm-6">
                                                    <div className="form-group">
                                                        <span className="form-label">Name</span>
                                                        <input className="form-control" type="text" placeholder="Enter your name" />
                                                    </div>
                                                </div>
                                                <div className="col-sm-6">
                                                    <div className="form-group">
                                                        <span className="form-label">Email</span>
                                                        <input className="form-control" type="email" placeholder="Enter your email" style={{width: '-webkit-fill-available'}} />
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="form-group">
                                                <span className="form-label">Phone</span>
                                                <input className="form-control" type="tel" placeholder="Enter your phone number" />
                                            </div>
                                            <div className="form-group">
                                                <span className="form-label">Ghi chú</span>
                                                <textarea id="subject" name="subject" placeholder="Write something.."
                                                          style={{width: '-webkit-fill-available', height: '100px'}} defaultValue={""}/>
                                            </div>

                                            <div className="row" >
                                                <div className="col-md-12 col-lg-12 col-sm-12">
                                                    <div className="col-sm-4">
                                                        <div className="form-group">
                                                            <span className="form-label"> Quantity</span>
                                                            <input type="number" name="quantity" min={1} max={20} style={{width: '-webkit-fill-available', paddingTop: '3px'}}  />
                                                        </div>
                                                    </div>
                                                    <div className="col-sm-4">
                                                        <div className="form-group">
                                                            <span className="form-label">Begin Date</span>
                                                            <input className="form-control" type="date" required  style={{width: '-webkit-fill-available'}} />
                                                        </div>
                                                    </div>
                                                    <div className="col-sm-4">
                                                        <div className="form-group">
                                                            <span className="form-label">End Date</span>
                                                            <input className="form-control" type="date" required  style={{width: '-webkit-fill-available'}} />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div className="form-btn">
                                                <button className="submit-btn">Book Now</button>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

const mapStateToProps = state => {
    return {
        isDisplayForm : state.isDisplayForm
    }
};

const mapDispatchToProps = (dispatch, props) => {
    return {
        onCloseForm : () => {
            dispatch(actions.closeForm());
        }
    }
}

export default connect(mapStateToProps,mapDispatchToProps)(BookingForm);