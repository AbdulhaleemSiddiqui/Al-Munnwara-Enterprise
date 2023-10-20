////import React from 'react';
////import { Document, Page, Text, View, StyleSheet, PDFViewer } from '@react-pdf/renderer';

////import faker from 'faker';



////function generateRandomInvoiceData() {
////    const customerName = faker.name.findName();
////    const invoiceNumber = faker.datatype.number();
////    const invoiceDate = faker.date.past().toLocaleDateString();
////    const dueDate = faker.date.future().toLocaleDateString();
////    const items = Array.from({ length: 5 }, () => ({
////        description: faker.commerce.productName(),
////        quantity: faker.datatype.number({ min: 1, max: 10 }),
////        price: faker.datatype.number({ min: 10, max: 100 }),
////    }));

////    return {
////        customerName,
////        invoiceNumber,
////        invoiceDate,
////        dueDate,
////        items,
////    };
////}

////const Invoice = () => {
////    const data = generateRandomInvoiceData();

////    return (
////        <Document>
////            <Page size="A4">
////                <View style={styles.header}>
////                    <Text style={styles.headerText}>Invoice</Text>
////                </View>
////                <View style={styles.content}>
////                    <Text>Customer Name: {data.customerName}</Text>
////                    <Text>Invoice Number: {data.invoiceNumber}</Text>
////                    <Text>Invoice Date: {data.invoiceDate}</Text>
////                    <Text>Due Date: {data.dueDate}</Text>

////                    <Text style={styles.itemHeader}>Items</Text>
////                    <View style={styles.itemTable}>
////                        {data.items.map((item, index) => (
////                            <View style={styles.itemRow} key={index.toString()}>
////                                <Text>{item.description}</Text>
////                                <Text>{item.quantity}</Text>
////                                <Text>{item.price}</Text>
////                            </View>
////                        ))}
////                    </View>
////                </View>
////            </Page>
////        </Document>
////    );
////};
////const styles = StyleSheet.create({
////    header: {
////        textAlign: 'center',
////        marginBottom: 20,
////    },
////    headerText: {
////        fontSize: 24,
////        fontWeight: 'bold',
////    },
////    content: {
////        marginLeft: 50,
////        marginRight: 50,
////    },
////});

////export default Invoice;