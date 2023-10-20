import React from 'react';
import {Text, View, StyleSheet } from '@react-pdf/renderer';

const styles = StyleSheet.create({
    headerContainer: {
        marginTop: 36
    },
    billTo: {
        marginTop: 20,
        paddingBottom: 3,
        fontFamily: 'Helvetica-Oblique'
    },
  });


  const BillTo = ({invoice}) => (
    <View style={styles.headerContainer}>
        <Text style={styles.billTo}>Bill To:</Text>
        <Text>{"Gets"}</Text>
        <Text>{"Karachi"}</Text>
        <Text>{+92310-2663662}</Text>
        <Text>{"Almunwwara@gmail.com"}</Text>
    </View>
  );
  
  export default BillTo