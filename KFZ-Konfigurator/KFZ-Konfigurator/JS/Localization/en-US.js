﻿export const messages = {
    app: {
        engineSettingsButton: 'Engines',
        accessoriesButton: 'Accessories',
        exteriorButton: 'Exterior',
    },
    configurationDocument: {
        technicalDataHeader: 'Technical Data',
        exteriorHeader: 'Exterior',
        accessoriesHeader: 'Accessories',
        basePriceHeader: 'Base Price',
        extrasPriceHeader: 'Extras Price',
        finalPriceHeader: 'Price',
        rimsHeader: 'Rims',
        paintsHeader: 'Paint',
    },
    orderListView: {
        header: 'Orders',
        carModelHeader: 'Model',
        descriptionHeader: 'Description',
        dateTimeHeader: 'Date',
        basePriceHeader: 'Base Price',
        extrasPriceHeader: 'Extras',
        priceHeader: 'Price',
        cancelConfirmTitle: 'Cancel Order',
        cancelConfirmText: 'Are you sure you want to cancel this order?',
    },
    orderOverviewView: {
        header: 'Your Order',
        orderSuccessfulHeader: 'Order successfully placed',
        orderSuccessfulDescription_2: 'You can also ',
        orderSuccessfulDescription_3: ' to view it later',
        copyOrderLinkButton: 'copy a link to your order',
    },
    configurationOverviewView: {
        header: 'Your Configuration',
        orderButton: 'Buy Your Configuration',
        placeOrderButton: 'Place Order',
        couldNotPlaceOrderError: 'Your order could not be placed',
        orderConfirmText: 'Are you sure you want to place this order?',
        orderConfirmTitle: 'Place Order',
        orderDescriptionPlaceholder: 'Discription (optional)'
    },
    modelView: {
        header: 'Choose your model',
        filterAll: 'All',
    },
    cartOverviewView: {
        extrasHeader: 'Extras',
        paintHeader: 'Paint',
        rimsHeader: 'Rims',
        basePriceLabel: 'Base Price',
        extrasPriceLabel: 'Extras Price',
        fullPriceLabel: 'Price',
        goToOverview: 'Show Overview',
    },
    exteriorView: {
        paintsHeader: 'Paint',
        rimsHeader: 'Rims',
        continueToOverviewButton: 'Continue To Overview'
    },
    engineSettingsView: {
        continueToAccessoriesButton: 'Continue To Accessories'
    },
    accessoriesView: {
        continueToExteriorButton: 'Continue To Exterior'
    },
    general: {
        yes: 'Yes',
        no: 'No',
        from: 'from',
        rimSizeUnit: 'inch',
        view: 'View',
        cancel: 'Cancel',
        productTitle: 'Car Configurator',
    },
    technicalData: {
        consumption: 'Consumption',
        emissions: 'Emissions',
        transmissionSuffix: '-Gear',
        transmissionAllroad: 'quattro',
        performance: 'Max. Performance',
        topSpeed: 'Top Speed',
        engineSize: 'Engine Size',
        transmission: 'Transmission',
        acceleration: 'Acceleration 0-100 km/h',
        topSpeed: 'Top Speed',
        fuelType: 'Fuel Type',
    },
    engineCategory: {
        TFSI: 'TFSI',
        TDI: 'TDI'
    },
    accessoryCategory: {
        AssistenceSystems: 'Assistence Systems',
        Comfort: 'Comfort',
        Infotainment: 'Infotainment',
        TechnologyAndSafety: 'Technology and Safety',
    },
    paintCategory: {
        Metalic: 'Metallic',
        Pearlescent: 'Pearlescent',
        Uni: 'Uni'
    },
    fuelCategory: {
        Diesel: 'Diesel',
        Petrol: 'Petrol',
    },
    columnName: {
        name: 'Name',
        category: 'Category',
        price: 'Price',
    }
};

export const numberFormats = {
    currency: {
        style: 'currency', currency: 'EUR'
    }
};

export const dateTimeFormats = {
    long: {
        year: 'numeric', month: 'short', day: 'numeric',
        weekday: 'short', hour: 'numeric', minute: 'numeric',
        hour12: false
    }
};