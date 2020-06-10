<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/schedule">
    <html>
      <head>
        <title>Расписание</title>
        <style>
          table{
          border-collapse: collapse;

          }
          td, th {
          border: 1px solid black;
          padding: 4px;
          width: 200px;
          text-align: center;
          }

        </style>
      </head>
      <body>
        <table>
          <tr>
            <th>Время</th>
            <xsl:apply-templates select="day"/>
          </tr>
          <tr>
            <td>8:10-9:30</td>
            <xsl:apply-templates select="day" mode="a">
              <xsl:with-param name="c" select="1"></xsl:with-param>
            </xsl:apply-templates>
          </tr>
          <tr>
            <td>9:40-11:00</td>
            <xsl:apply-templates select="day" mode="a">
              <xsl:with-param name="c" select="2"></xsl:with-param>
            </xsl:apply-templates>
          </tr>
          <tr>
            <td>11:30-12:50</td>
            <xsl:apply-templates select="day" mode="a">
              <xsl:with-param name="c" select="3"></xsl:with-param>
            </xsl:apply-templates>
          </tr>
          <tr>
            <td>13:00-14:20</td>
            <xsl:apply-templates select="day" mode="a">
              <xsl:with-param name="c" select="4"></xsl:with-param>
            </xsl:apply-templates>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="day">
    <th>
      <xsl:value-of select="@weekday"/>
    </th>
  </xsl:template>

  <xsl:template match="day" mode="a">
    <xsl:param name="c"/>
    <xsl:choose>
      <xsl:when test="subject[@number=$c]">
        <xsl:apply-templates select="subject[@number=$c]"/>
      </xsl:when>
      <xsl:otherwise>
        <td></td>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="day/subject">
    <td>
      <xsl:value-of select="name"/>
      <br/>
      <xsl:text>(</xsl:text>
      <xsl:value-of select="@type"/>
      <xsl:text>)</xsl:text>
      <br/>
      <xsl:value-of select="class"/>
      <br/>
      <xsl:value-of select="teacher"/>
    </td>
  </xsl:template>
</xsl:stylesheet>