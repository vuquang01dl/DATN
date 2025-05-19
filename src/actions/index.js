import * as Types from './../constants/ActionType';
import callApi from './../utils/apiCaller';

export const getAllTourAPI = () => {
    return (dispatch) => {
        return callApi('/tours', 'GET', null)
            .then(res => {
                const fakeData = [
                    { id: 1, name: "Tour Đà Nẵng", price: 3000000 },
                    { id: 2, name: "Tour Huế", price: 2500000 }
                ];

                // Nếu res hoặc res.data không tồn tại thì dùng fakeData
                const data = (res && res.data) ? res.data : fakeData;
                dispatch(getAllTour(data));
            })
            .catch(err => {
                console.error("API error:", err);
                const fakeData = [
                    { id: 1, name: "Tour Đà Nẵng", price: 3000000 },
                    { id: 2, name: "Tour Huế", price: 2500000 }
                ];
                dispatch(getAllTour(fakeData));
            });
    }
}


export const getAllTour = (tours) => {
    return {
        type: Types.GET_ALL_TOUR,
        tours
    }
}

export const getAllCategoryAPI = () => {
    return (dispatch) => {
        const mockCategories = [
            { id: 1, name: 'Du lịch biển' },
            { id: 2, name: 'Núi rừng' },
            { id: 3, name: 'Thành phố' }
        ];

        dispatch(getAllCategory(mockCategories));
    }
};


export const getAllCategory = (categories) => {
    return {
        type: Types.GET_ALL_CATEGORY,
        categories
    }
}

export const getAllUserAPI = () => {
    return (dispatch) => {
        return callApi('/users', 'GET', null).then(res => {
            dispatch(getAllUser(res.data))
        });
    }
}

export const getAllUser = (users) => {
    return {
        type: Types.GET_ALL_USER,
        users
    }
}

export const openForm = () => {
    return {
        type : Types.OPEN_BOOKING_FORM
    }
}
export const closeForm = () => {
    return {
        type : Types.CLOSE_BOOKING_FORM
    }
}
export const userLogout = () => {
    return {
        type : Types.LOG_OUT
    }
}
